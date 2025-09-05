using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {
        // This is the mock object of LeaveTypeRepository
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test Vacation",
                },
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test Sick",
                },
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test Maternity",
                }
            };
            // this is a mock of the repo it means that none of the methods are actually going to be implemented or it's not going to use the implementations as
            // we would have set up in our persistence. It only know this abstraction
            var mockRepo = new Mock<ILeaveTypeRepository>();

            // TO make it know what it is suppose to do
            // here we are configuring while testing this is the implementation for this method in test and it should return mock objects of type leave type or our list.
            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);
            // Here we are testing the create method and sice it takes a parameter we pass the It.IsAny<T> and we pass the type of the object we want to mock
            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                // Here we are capturing that leaf type that would have been passed during the testing.
                //And we add it to the leaveTypes list
                .Returns((LeaveType leaveType) => 
                { 
                    leaveTypes.Add(leaveType);
                    // since createAsync has a return type Task we have to return like this:
                    return Task.CompletedTask;
                });
            // it means that if during the runtime there is three things in database, then i call CreateAsync with a new object and it gets to the data base
            // then it should add that fourth one to the database.

            return mockRepo;
            
        }
    }
}
