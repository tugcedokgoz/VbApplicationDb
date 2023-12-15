Imports VbApplication.Modell

Public Interface ISupplierBs
    Function GetAllSupplierAsync() As Task(Of List(Of SupplierGetDto))
    Function PostSupplierAsync(SupplierPostDto As SupplierPostDto) As Task(Of SupplierPostDto)
    Function GetUserById(userid As Long) As Task(Of SupplierGetDto)
    Sub UpdateSupplierAsync(SupplierPostDto As SupplierPostDto)
    Sub DeleteAsync(supplierid As Long)
End Interface
