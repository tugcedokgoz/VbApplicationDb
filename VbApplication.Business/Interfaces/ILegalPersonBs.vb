Imports VbApplication.Modell

Public Interface ILegalPersonBs
    Function GetAllLegalPersonAsync() As Task(Of List(Of LegalPersonGetDto))
    Function PostLegalPersonAsync(legalPersonPostDto As LegalPersonPostDto) As Task(Of LegalPersonPostDto)
    Function GetUserById(userid As Long) As Task(Of LegalPersonGetDto)
    Sub UpdateLegalPersonAsync(legalPersonPostDto As LegalPersonPostDto)
    Sub DeleteAsync(userid As Long)
End Interface
