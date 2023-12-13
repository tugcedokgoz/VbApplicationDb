Imports AutoMapper
Imports VbApplication.DataAccess
Imports VbApplication.Modell
Imports VbApplication.Modell.Models

Public Class EmployeeBs
    Implements IEmployeeBs
    Private ReadOnly _userRepository As IUserRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(userRepository As IUserRepository, mapper As IMapper)
        _userRepository = userRepository
        _mapper = mapper
    End Sub

    Public Sub UpdateEmployeeAsync(employeePostDto As EmployeePostDto) Implements IEmployeeBs.UpdateEmployeeAsync
        ' AutoMapper kullanarak UserPostDto'yu User sınıfına dönüştür
        Dim updatedEmployee As User = _mapper.Map(Of User)(employeePostDto)

        ' Kullanıcıyı veritabanında güncelleme işlemi için uygun bir metodun çağrılması
        _userRepository.Update(updatedEmployee)
    End Sub

    Public Async Sub DeleteAsync(userid As Long) Implements IEmployeeBs.DeleteAsync
        Dim employee As User = Await _userRepository.GetById(userid)

        ' Kullanıcı var mı diye kontrol yapın
        If employee IsNot Nothing Then
            ' Kullanıcıyı veritabanından silme işlemi için uygun bir metodun çağrılması
            _userRepository.Delete(userid)
        Else
            ' Kullanıcı bulunamadıysa uygun bir işlemi gerçekleştirin (loglama, hata mesajı, vb.)
            Throw New InvalidOperationException($"Employee with ID {userid} not found.")
        End If
    End Sub

    Public Async Function GetAllEmployeeAsync() As Task(Of List(Of EmployeeGetDto)) Implements IEmployeeBs.GetAllEmployeeAsync
        Dim users = Await _userRepository.GetAllAsync()
        Dim filteredEmployees = users.Where(Function(u) u.Active.HasValue AndAlso
                                                      u.Active.Value AndAlso
                                                      Not String.IsNullOrEmpty(u.Department) AndAlso
                                                      Not String.IsNullOrEmpty(u.EmpPosition) AndAlso
                                                      u.EmpSalary.HasValue).ToList()

        Dim employeeDtos As List(Of EmployeeGetDto) = _mapper.Map(Of List(Of EmployeeGetDto))(filteredEmployees)
        Return employeeDtos
    End Function

    Public Async Function PostEmployeeAsync(employeePostDto As EmployeePostDto) As Task(Of EmployeePostDto) Implements IEmployeeBs.PostEmployeeAsync
        Dim newEmployee As User = _mapper.Map(Of User)(employeePostDto)

        Await _userRepository.Post(newEmployee)
    End Function

    Public Async Function GetUserById(userid As Long) As Task(Of EmployeeGetDto) Implements IEmployeeBs.GetUserById
        ' Kullanıcıyı (Employee) veritabanından id'ye göre çek
        Dim employee As User = Await _userRepository.GetById(userid)

        ' Eğer employee bulunamazsa veya aktif değilse veya gerekli özelliklere sahip değilse, null döndür veya hata fırlat
        If employee Is Nothing OrElse Not employee.Active.HasValue OrElse Not employee.Active.Value OrElse
           String.IsNullOrEmpty(employee.Department) OrElse
           String.IsNullOrEmpty(employee.EmpPosition) OrElse
           employee.EmpSalary Is Nothing Then
            Throw New KeyNotFoundException($"Active employee with ID {userid} and the required properties not found.")
        End If

        ' AutoMapper kullanarak User nesnesini EmployeeGetDto'ya dönüştür
        Dim employeeDto As EmployeeGetDto = _mapper.Map(Of EmployeeGetDto)(employee)

        Return employeeDto
    End Function
End Class
