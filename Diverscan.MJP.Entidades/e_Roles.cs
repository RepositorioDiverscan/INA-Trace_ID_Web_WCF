using System;

namespace Diverscan.MJP.Entidades
{
    public class e_Roles
    {
        public string idRol { get; set; }
        public string NombreRol { get; set; }
        public bool wf_Usuario { get; set; }
        public bool wf_Default { get; set; }
       // public bool wf_Requisicion { get; set; }
        /// <summary>
        /// Autor Fernando Torres Descripcion: Creacion de la entidad aplicacble a todos los forms
        /// </summary>
        #region Ingresos
        public bool wf_Custodia { set; get; }
        public bool wf_Devolucion { set; get; }
        public bool wf_MenuIngresos { set; get; }
        public bool wf_DetalleCompra { set; get; }
        public bool wf_ListadoOrdenes { set; get; }
        public bool wf_OrdenCompra { set; get; }
        #endregion

        #region Administracion
        public bool wf_MenuAdministracion { set; get; }
        public bool wf_Usuarios { set; get; }
        public bool wf_Credenciales { set; get; }
        public bool wf_FamiliarYRequerimientos { set; get; }
        public bool wf_Programa { set; get; }
        public bool wf_CentrolesPenales { set; get; }
        #endregion

        #region Salidas
        public bool wf_MenuSalidas { set; get; }
        public bool wf_Alistos { set; get; }
        public bool wf_ConsultaAlistos { set; get; }
        public bool wf_Despacho { set; get; }
        public bool wf_ConsultaRequisicion { set; get; }
        public bool wf_Requisicion{ set; get; }
        public bool wf_DetalleRequisicion { set; get; }
        #endregion

        #region Configuracion
        public bool wf_MenuConfiguracion { get; set; }
        public bool wf_Inventario { get; set; }
        public bool wf_Localizacion { get; set; }
        public bool wf_MantenimientoGeneral { get; set; }
        public bool wf_MenuUbicaciones { get; set; }
        public bool wf_Reubicacion { get; set; }
        public bool wf_ReporteUbicacion { get; set; }
        public bool wf_ListadoRequerimientos { get; set; }
        public bool wf_ValidaRequerimientos { get; set; }
        public bool wf_TrasladoProgramas { get; set; }
        #endregion

        #region Embarque
        public bool wf_CargarEmbarque { get; set; }
        public bool wf_FormularioEmbarque { get; set; }
        public bool wf_ListadoEmbarque { get; set; }
        #endregion

        #region MaestroArticulos
        public bool wf_BandejaMaestroArticulo { get; set; }
        public bool wf_Empaque { get; set; }
        public bool wf_ImporatarExcel { get; set; }
        public bool wf_MaestroArticulo { get; set; }
        #endregion

        #region "HH"
        public bool rl_ManejoHH { get; set; }
        #endregion
    }
}
