export interface promoModel {
  idPromocion: number,
  nombre: string,
  descripcion: string,
  fechaInicio: string,
  fechaFin: string,
  tipoDescuento: string,
  valorDescuento: number,
  aplicaA: string,
  duracionMeses: number,
  idServicios: (object)[]
}
