Imports AutoMapper
Imports VbApplication.DataAccess
Imports VbApplication.Modell
Imports VbApplication.Modell.Models

Public Class InternBs
    Implements IInternBs
    Private ReadOnly _userRepository As IUserRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(userRepository As IUserRepository, mapper As IMapper)
        _userRepository = userRepository
        _mapper = mapper
    End Sub

    Public Sub UpdateInternAsync(internPostDto As InternPostDto) Implements IInternBs.UpdateInternAsync
        ' AutoMapper kullanarak UserPostDto'yu User sınıfına dönüştür
        Dim updatedIntern As User = _mapper.Map(Of User)(internPostDto)

        ' Kullanıcıyı veritabanında güncelleme işlemi için uygun bir metodun çağrılması
        _userRepository.Update(updatedIntern)
    End Sub

    Public Async Sub DeleteAsync(userid As Long) Implements IInternBs.DeleteAsync
        Dim intern As User = Await _userRepository.GetById(userid)

        ' Kullanıcı var mı diye kontrol yapın
        If intern IsNot Nothing Then
            ' Kullanıcıyı veritabanından silme işlemi için uygun bir metodun çağrılması
            _userRepository.Delete(userid)
        Else
            ' Kullanıcı bulunamadıysa uygun bir işlemi gerçekleştirin (loglama, hata mesajı, vb.)
            Throw New InvalidOperationException($"Intern with ID {userid} not found.")
        End If
    End Sub

    Public Async Function GetAllInternAsync() As Task(Of List(Of InternGetDto)) Implements IInternBs.GetAllInternAsync
        Dim users = Await _userRepository.GetAllAsync()
        Dim filteredInterns = users.Where(Function(u) u.Active.HasValue AndAlso
                                                      u.Active.Value AndAlso
                                                      Not String.IsNullOrEmpty(u.Department) AndAlso
                                                      Not String.IsNullOrEmpty(u.InternSchool) AndAlso
                                                      Not String.IsNullOrEmpty(u.InternGrade)).ToList()

        Dim internDtos As List(Of InternGetDto) = _mapper.Map(Of List(Of InternGetDto))(filteredInterns)
        Return internDtos
    End Function

    Public Async Function PostInternAsync(internPostDto As InternPostDto) As Task(Of InternPostDto) Implements IInternBs.PostInternAsync
        Dim newIntern As User = _mapper.Map(Of User)(internPostDto)

        Await _userRepository.Post(newIntern)
    End Function

    Public Async Function GetUserById(userid As Long) As Task(Of InternGetDto) Implements IInternBs.GetUserById
        ' Kullanıcıyı (Intern) veritabanından id'ye göre çek
        Dim intern As User = Await _userRepository.GetById(userid)

        ' Eğer intern bulunamazsa veya aktif değilse veya gerekli özelliklere sahip değilse, null döndür veya hata fırlat
        If intern Is Nothing OrElse Not intern.Active.HasValue OrElse Not intern.Active.Value OrElse
           String.IsNullOrEmpty(intern.Department) OrElse
           String.IsNullOrEmpty(intern.InternSchool) OrElse
           String.IsNullOrEmpty(intern.InternGrade) Then
            Throw New KeyNotFoundException($"Active intern with ID {userid} and the required properties not found.")
        End If

        ' AutoMapper kullanarak User nesnesini InternGetDto'ya dönüştür
        Dim internDto As InternGetDto = _mapper.Map(Of InternGetDto)(intern)

        Return internDto
    End Function
End Class
