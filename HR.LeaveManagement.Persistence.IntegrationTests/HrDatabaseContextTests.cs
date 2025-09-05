using HR.LeaveManagement.Domain;
using HR.LeaveManagment.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests
{
    public class HrDatabaseContextTests
    {
        private HrDatabaseContext _hrDatabaseContext;

        public HrDatabaseContextTests()
        {
            // this creates the DBcontext in memory 
            var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _hrDatabaseContext = new HrDatabaseContext(dbOptions);
        }
        
        // This test case makes sure that the date created gets set
        [Fact]
        public async void Save_SetCreatedValue()
        {
            // Arrange part of a test
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation",
            };
            // Act 
            await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
            await _hrDatabaseContext.SaveChangesAsync();

            // Assert 
            leaveType.DateCreated.ShouldNotBeNull();
        }
        // This test case makes sure that the date Modified gets set
        [Fact]
        public async Task Save_SetModifiedValueAsync()
        {
            // Arrange part of a test
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation",
            };
            // Act 
            await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
            await _hrDatabaseContext.SaveChangesAsync();

            // Assert 
            leaveType.DateModified.ShouldNotBeNull();
        }
    }
}