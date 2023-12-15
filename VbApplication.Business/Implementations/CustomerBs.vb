Imports AutoMapper
Imports VbApplication.DataAccess
Imports VbApplication.Modell
Imports VbApplication.Modell.Models

Public Class CustomerBs
    Implements ICustomerBs
    Private ReadOnly _userRepository As IUserRepository

    Public Sub New(userRepository As IUserRepository, mapper As IMapper)
        _userRepository = userRepository
        _mapper = mapper
    End Sub

    Private ReadOnly _mapper As IMapper
    Public Async Sub UpdateCustomerAsync(customerPostDto As CustomerPostDto) Implements ICustomerBs.UpdateCustomerAsync
        ' AutoMapper kullanarak UserPostDto'yu User sınıfına dönüştür
        Dim updatedCustomer As User = _mapper.Map(Of User)(customerPostDto)

        ' Kullanıcıyı veritabanında güncelleme işlemi için uygun bir metodun çağrılması
        _userRepository.Update(updatedCustomer)
    End Sub

    Public Async Sub DeleteAsync(customerid As Long) Implements ICustomerBs.DeleteAsync
        Dim customer = Await _userRepository.GetById(customerid)

        If customer IsNot Nothing AndAlso customer.Active.HasValue AndAlso customer.Active.Value Then
            customer.CustTitle = Nothing
            customer.CustGender = Nothing
            customer.CustDateOfBirth = Nothing

            Await _userRepository.SaveChangesAsync()
        End If
    End Sub

    Public Async Function GetAllCustomersAsync() As Task(Of List(Of CustomerGetDto)) Implements ICustomerBs.GetAllCustomersAsync
        Dim users = Await _userRepository.GetAllAsync()
        Dim filteredUsers = users.Where(Function(u) u.Active.HasValue AndAlso
                                                   u.Active.Value AndAlso
                                                   Not String.IsNullOrEmpty(u.CustTitle) AndAlso
                                                   Not String.IsNullOrEmpty(u.CustGender) AndAlso
                                                   u.CustDateOfBirth.HasValue).ToList()

        Dim customerDtos As List(Of CustomerGetDto) = _mapper.Map(Of List(Of CustomerGetDto))(filteredUsers)
        Return customerDtos
    End Function

    Public Async Function PostCustomerAsync(customerGetDto As CustomerPostDto) As Task(Of CustomerPostDto) Implements ICustomerBs.PostCustomerAsync
        Dim newCustomer As User = _mapper.Map(Of User)(customerGetDto)

        Await _userRepository.Post(newCustomer)
    End Function

    Public Async Function GetUserById(userid As Long) As Task(Of CustomerGetDto) Implements ICustomerBs.GetUserById
        ' Kullanıcıyı (Customer) veritabanından id'ye göre çek
        Dim customer As User = Await _userRepository.GetById(userid)

        ' Eğer customer bulunamazsa veya aktif değilse veya gerekli özelliklere sahip değilse, null döndür veya hata fırlat
        If customer Is Nothing OrElse Not customer.Active.HasValue OrElse Not customer.Active.Value OrElse
           String.IsNullOrEmpty(customer.CustTitle) OrElse
           String.IsNullOrEmpty(customer.CustGender) OrElse
           customer.CustDateOfBirth Is Nothing Then
            Throw New KeyNotFoundException($"Active customer with ID {userid} and the required properties not found.")
        End If

        ' AutoMapper kullanarak User nesnesini CustomerGetDto'ya dönüştür
        Dim customerDto As CustomerGetDto = _mapper.Map(Of CustomerGetDto)(customer)

        Return customerDto
    End Function
End Class
