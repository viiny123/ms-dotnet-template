using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Template.Application.Base.Error;
using Template.Application.Value.V1.Commands.Create;
using Template.Bootstrap.Configurations;
using Template.Domain.AggregatesModel.ValueAggreate;
using Template.Domain.Base;
using Xunit;

namespace Template.Application.Tests.Value.V1.Commands.Create;

public class CreateValueCommandHandlerTests
{

    private readonly Mock<IValueRepository> mockValueRespository;
    private readonly Mock<IUnitOfWork> mockUnitOfWork;

    public CreateValueCommandHandlerTests()
    {
        var services = new ServiceCollection();
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Test.json");

        var configuration = configurationBuilder.Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.ConfigureDatabases(configuration);

        mockValueRespository = new Mock<IValueRepository>();
        mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task Create_CreateValue_ReturnValueId()
    {
        // Arrange
        var request = new CreateValueCommand("Code-99", "Description-99");

        // Act
        var handler = new CreateValueCommandHandler(mockValueRespository.Object, mockUnitOfWork.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsValid);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task Create_CreateValue_IsInivalidWithNegativeCode()
    {
        // Arrange
        var request = new CreateValueCommand("-1", "Description-1");
        var validationList = new List<ErrorDetail>
        {
            new ErrorDetail(ErrorCatalog.Value.CodeCanBeNegativeNumber)
        };

        // Act
        var handler = new CreateValueCommandHandler(mockValueRespository.Object, mockUnitOfWork.Object);
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsValid);
        Assert.Null(result.Data);
        Assert.Collection(validationList,
                item => Assert.Equal(ErrorCatalog.Value.CodeCanBeNegativeNumber.Code, item.ErrorCode)
            );

    }
}
