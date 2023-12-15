Imports VbApplication.Modell

Public Interface ICustomerBs
    Function GetAllCustomersAsync() As Task(Of List(Of CustomerGetDto))
    Function PostCustomerAsync(customerPostDto As CustomerPostDto) As Task(Of CustomerPostDto)
    Function GetUserById(userid As Long) As Task(Of CustomerGetDto)
    Sub UpdateCustomerAsync(customerPostDto As CustomerPostDto)
    Sub DeleteAsync(customerid As Long)
End Interface
