Imports VbApplication.Modell.Models


Public Interface IUserRepository
    Function GetAllAsync() As Task(Of List(Of User))
    Function Post(Of T)(userDto As T) As Task(Of User)
    Function GetById(userId As Long) As Task(Of User)
    Sub Update(updatedUser As User)


    Sub Delete(userId As Long)
    Function FindUserByNameAsync(userName As String) As Task(Of User)

    Function SaveChangesAsync() As Task
    Function LoginAsync(userName As String) As Task(Of User)
End Interface

