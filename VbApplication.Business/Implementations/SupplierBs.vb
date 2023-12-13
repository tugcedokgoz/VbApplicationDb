Imports AutoMapper
Imports VbApplication.DataAccess
Imports VbApplication.Modell
Imports VbApplication.Modell.Models

Public Class SupplierBs
    Implements ISupplierBs
    Private ReadOnly _userRepository As IUserRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(userRepository As IUserRepository, mapper As IMapper)
        _userRepository = userRepository
        _mapper = mapper
    End Sub

    Public Sub UpdateSupplierAsync(SupplierPostDto As SupplierPostDto) Implements ISupplierBs.UpdateSupplierAsync
        ' AutoMapper kullanarak UserPostDto'yu User sınıfına dönüştür
        Dim updatedSupplier As User = _mapper.Map(Of User)(SupplierPostDto)

        ' Kullanıcıyı veritabanında güncelleme işlemi için uygun bir metodun çağrılması
        _userRepository.Update(updatedSupplier)
    End Sub

    Public Async Sub DeleteAsync(userid As Long) Implements ISupplierBs.DeleteAsync
        Dim supplier As User = Await _userRepository.GetById(userid)

        ' Kullanıcı var mı diye kontrol yapın
        If supplier IsNot Nothing Then
            ' Kullanıcıyı veritabanından silme işlemi için uygun bir metodun çağrılması
            _userRepository.Delete(userid)
        Else
            ' Kullanıcı bulunamadıysa uygun bir işlemi gerçekleştirin (loglama, hata mesajı, vb.)
            Throw New InvalidOperationException($"Supplier with ID {userid} not found.")
        End If
    End Sub

    Public Async Function GetAllSupplierAsync() As Task(Of List(Of SupplierGetDto)) Implements ISupplierBs.GetAllSupplierAsync
        Dim users = Await _userRepository.GetAllAsync()
        Dim filteredSuppliers = users.Where(Function(u) u.Active.HasValue AndAlso
                                                      u.Active.Value AndAlso
                                                      Not String.IsNullOrEmpty(u.SupContactName) AndAlso
                                                      Not String.IsNullOrEmpty(u.SupContactSurname) AndAlso
                                                      Not String.IsNullOrEmpty(u.SupContactTitle)).ToList()

        Dim supplierDtos As List(Of SupplierGetDto) = _mapper.Map(Of List(Of SupplierGetDto))(filteredSuppliers)
        Return supplierDtos
    End Function

    Public Async Function PostSupplierAsync(SupplierPostDto As SupplierPostDto) As Task(Of SupplierPostDto) Implements ISupplierBs.PostSupplierAsync
        Dim newSupplier As User = _mapper.Map(Of User)(SupplierPostDto)

        Await _userRepository.Post(newSupplier)
    End Function

    Public Async Function GetUserById(userid As Long) As Task(Of SupplierGetDto) Implements ISupplierBs.GetUserById
        Dim supplier As User = Await _userRepository.GetById(userid)

        ' Eğer supplier bulunamazsa veya aktif değilse veya gerekli özelliklere sahip değilse, null döndür veya hata fırlat
        If supplier Is Nothing OrElse Not supplier.Active.HasValue OrElse Not supplier.Active.Value OrElse
           String.IsNullOrEmpty(supplier.SupContactName) OrElse
           String.IsNullOrEmpty(supplier.SupContactSurname) OrElse
           String.IsNullOrEmpty(supplier.SupContactTitle) Then
            Throw New KeyNotFoundException($"Active supplier with ID {userid} and the required properties not found.")
        End If

        ' AutoMapper kullanarak User nesnesini SupplierGetDto'ya dönüştür
        Dim supplierDto As SupplierGetDto = _mapper.Map(Of SupplierGetDto)(supplier)

        Return supplierDto
    End Function
End Class
