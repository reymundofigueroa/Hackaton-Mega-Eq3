export interface promoModel {
  idPromocion: number,
  nombre: string,
  descripcion: string,
  fechaInicio: string,
  fechaFin: string,
  tipoDescuento: string,
  valorDescuento: number,
  servicios: servicioModel[]
  aplicaA: string,
  duracionMeses: number,
  idServicios?: any[]
}

export interface servicioModel {
  idServicio: number,
  nombre: string,
  descripcion: string,
  precioBaseActual: number
}

export interface clientModel {
  idSuscriptor: number;
  nombre: string;
  apellidoPaterno: string;
  apellidoMaterno: string;
  rfc: string;
  email: string;
  telefonoContacto: string;
  fechaRegistro: string;
  idDomicilio: number;
}
