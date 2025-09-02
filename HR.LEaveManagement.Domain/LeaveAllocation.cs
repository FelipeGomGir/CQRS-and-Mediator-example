using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain;

public class LeaveAllocation: BaseEntity
{
    public string EmployeeId { get; set; } = string.Empty;
    public string NumberOfDays { get; set; }
    public LeaveType? LeaveType { get; set; } /// This is a foreing key between LeaveType and Leave Allocation 
    public int LeaveTypeId { get; set; } /// This is a foreing key between LeaveType and Leave Allocation 
    public int Period { get; set; }
}
