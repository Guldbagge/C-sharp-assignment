namespace ConsoleApp.Interface
{
    public interface IEmployeeService
    {
        void AddEmployee(Employee employee);
        Employee GetEmployee(int id);
        List<Employee> GetEmployees();
        bool RemoveEmployee(int id);
        void UpdateEmployee(Employee employee);
    }
}