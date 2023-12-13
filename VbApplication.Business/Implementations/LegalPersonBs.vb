Imports AutoMapper
Imports VbApplication.DataAccess
Imports VbApplication.Modell
Imports VbApplication.Modell.Models

Public Class LegalPersonBs
    Implements ILegalPersonBs
    Private ReadOnly _userRepository As IUserRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(userRepository As IUserRepository, mapper As IMapper)
        _userRepository = userRepository
        _mapper = mapper
    End Sub

    Public Sub UpdateLegalPersonAsync(legalPersonPostDto As LegalPersonPostDto) Implements ILegalPersonBs.UpdateLegalPersonAsync
        ' AutoMapper kullanarak UserPostDto'yu User sınıfına dönüştür
        Dim updatedLegalPerson As User = _mapper.Map(Of User)(legalPersonPostDto)

        ' Kullanıcıyı veritabanında güncelleme işlemi için uygun bir metodun çağrılması
        _userRepository.Update(updatedLegalPerson)
    End Sub

    Public Async Sub DeleteAsync(userid As Long) Implements ILegalPersonBs.DeleteAsync
        Dim legalPerson As User = Await _userRepository.GetById(userid)

        ' Kullanıcı var mı diye kontrol yapın
        If legalPerson IsNot Nothing Then
            ' Kullanıcıyı veritabanından silme işlemi için uygun bir metodun çağrılması
            _userRepository.Delete(userid)
        Else
            ' Kullanıcı bulunamadıysa uygun bir işlemi gerçekleştirin (loglama, hata mesajı, vb.)
            Throw New InvalidOperationException($"Legal Person with ID {userid} not found.")
        End If
    End Sub

    Public Async Function GetAllLegalPersonAsync() As Task(Of List(Of LegalPersonGetDto)) Implements ILegalPersonBs.GetAllLegalPersonAsync
        Dim users = Await _userRepository.GetAllAsync()
        Dim filteredLegalPersons = users.Where(Function(u) u.Active.HasValue AndAlso
                                                         u.Active.Value AndAlso
                                                         Not String.IsNullOrEmpty(u.LegalpersonCompanyName) AndAlso
                                                         Not String.IsNullOrEmpty(u.LegalPersonTaxNumber) AndAlso
                                                         Not String.IsNullOrEmpty(u.LegalPersonAddress) AndAlso
                                                         Not String.IsNullOrEmpty(u.LegalPersonPhone) AndAlso
                                                         Not String.IsNullOrEmpty(u.LegalPersonFax)).ToList()

        Dim legalPersonDtos As List(Of LegalPersonGetDto) = _mapper.Map(Of List(Of LegalPersonGetDto))(filteredLegalPersons)
        Return legalPersonDtos
    End Function

    Public Async Function PostLegalPersonAsync(legalPersonPostDto As LegalPersonPostDto) As Task(Of LegalPersonPostDto) Implements ILegalPersonBs.PostLegalPersonAsync
        Dim newLegalPerson As User = _mapper.Map(Of User)(legalPersonPostDto)

        Await _userRepository.Post(newLegalPerson)
    End Function

    Public Async Function GetUserById(userid As Long) As Task(Of LegalPersonGetDto) Implements ILegalPersonBs.GetUserById
        ' Kullanıcıyı (LegalPerson) veritabanından id'ye göre çek
        Dim legalPerson As User = Await _userRepository.GetById(userid)

        ' Eğer legalPerson bulunamazsa veya aktif değilse veya gerekli özelliklere sahip değilse, null döndür veya hata fırlat
        If legalPerson Is Nothing OrElse Not legalPerson.Active.HasValue OrElse Not legalPerson.Active.Value OrElse
           String.IsNullOrEmpty(legalPerson.LegalpersonCompanyName) OrElse
           String.IsNullOrEmpty(legalPerson.LegalPersonTaxNumber) OrElse
           String.IsNullOrEmpty(legalPerson.LegalPersonAddress) OrElse
           String.IsNullOrEmpty(legalPerson.LegalPersonPhone) OrElse
           String.IsNullOrEmpty(legalPerson.LegalPersonFax) Then
            Throw New KeyNotFoundException($"Active legal person with ID {userid} and the required properties not found.")
        End If

        ' AutoMapper kullanarak User nesnesini LegalPersonGetDto'ya dönüştür
        Dim legalPersonDto As LegalPersonGetDto = _mapper.Map(Of LegalPersonGetDto)(legalPerson)

        Return legalPersonDto
    End Function
End Class
