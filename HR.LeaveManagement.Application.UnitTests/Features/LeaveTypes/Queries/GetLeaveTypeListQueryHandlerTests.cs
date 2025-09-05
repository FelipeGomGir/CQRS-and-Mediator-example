using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries
{
    public class GetLeaveTypeListQueryHandlerTests
    {
        // it should contain dependencies we are testing ( ).
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private IMapper _mapper;
        private readonly Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockAppLogger;

        public GetLeaveTypeListQueryHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            // To get a map instance for the test what is needed to do is to load up a mapper config.
            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile<LeaveTypeProfile>();

            // Create a logger factory (you can use NullLoggerFactory in tests)
            var loggerFactory = LoggerFactory.Create(builder => { });
            // Or: var loggerFactory = NullLoggerFactory.Instance;

            // Build the mapper configuration with expression + loggerFactory
            var mapperConfig = new MapperConfiguration(configExpression, loggerFactory);

            _mapper = mapperConfig.CreateMapper();
            _mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();

        }
        // Once we have the constructor with all the dependencies mocked we proceed to create the method 
        [Fact]
        public async Task GetLeaveTypeListTest()
        {
            // we instantiate the handler
            var handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepo.Object,
                _mockAppLogger.Object);
            // recieve the result from the handler. 
            var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveTypeDto>>();
            result.Count().ShouldBe(3);
        }
        // The test validated that the handler in reality will use the repository to add a new item to a list. 
    }

}
