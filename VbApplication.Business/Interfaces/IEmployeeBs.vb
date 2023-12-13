Imports VbApplication.Modell

Public Interface IEmployeeBs
    Function GetAllEmployeeAsync() As Task(Of List(Of EmployeeGetDto))
    Function PostEmployeeAsync(employeePostDto As EmployeePostDto) As Task(Of EmployeePostDto)
    Function GetUserById(userid As Long) As Task(Of EmployeeGetDto)
    Sub UpdateEmployeeAsync(employeePostDto As EmployeePostDto)
    Sub DeleteAsync(userid As Long)
End Interface
