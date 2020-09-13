<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Modal.ascx.vb" Inherits="FoodBusinessManager.Modal" %>

<!-- Inicio Modal -->
<div class="modal fade" id="ucMensajeModal" tabindex="-1" role="dialog" aria-labelledby="ucMensajeModal" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
   
            <div class="modal-header">
                <h5 id="tituloModal" class="modal-title " runat="server">FBM</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body text-info" id="BodyModal" runat="server">
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnPrincipal" runat="server" CssClass="btn btn-outline-info" Text="Aceptar" Visible="true" />
                <asp:Button ID="btnSecundario" runat="server" CssClass="btn btn-outline-danger" Text="Cancelar" Visible="true" />
            </div>
        </div>
    </div>
</div>
<!-- Fin Modal -->
