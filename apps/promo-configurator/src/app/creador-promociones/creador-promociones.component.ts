import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { UbicacionService, Estado, Municipio, Ciudad, Colonia, Sucursal } from '../services/location/ubicacion.service';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
@Component({
  selector: 'app-creador-promociones',
  standalone: true,
  imports: [CommonModule, FormsModule, NavBarComponent],
  templateUrl: './creador-promociones.component.html',
  styleUrls: ['./creador-promociones.component.css']
})
export class CreadorPromocionesComponent implements OnInit {
  // Campos del formulario
  nombre: string = '';
  descripcionDescuento: string = '';
  fechaInicio: string = '';
  fechaFin: string = '';
  tipoDescuento: 'monto' | 'porcentaje' | 'gratis' = 'monto';
  valorDescuento: number = 0;
  mesesGratis: number = 1;
  duracionSeleccionada: number = 1;
  aplicaMensualidad = false;
  aplicaInstalacion = false;
  servicioSeleccionado: string | null = null;

  // Lógica de ubicación
  infoUbicacionCompleta: any = null;
  estados: Estado[] = [];
  municipios: Municipio[] = [];
  ciudades: Ciudad[] = [];
  colonias: Colonia[] = [];
  sucursales: Sucursal[] = [];
  servicios: any[] = [];

  estadoSeleccionado?: Estado;
  municipioSeleccionado?: Municipio;
  ciudadSeleccionada?: Ciudad;
  coloniaSeleccionada?: Colonia;

  municipiosFiltrados: Municipio[] = [];
  ciudadesFiltradas: Ciudad[] = [];
  coloniasFiltradas: Colonia[] = [];
  sucursalesFiltradas: Sucursal[] = [];

  duracionMeses: number[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];

  @ViewChild('autoResize') textarea!: ElementRef<HTMLTextAreaElement>;

  constructor(
    private ubicacionService: UbicacionService,
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.ubicacionService.getUbicacionCompleta().subscribe({
      next: (data) => {
        this.infoUbicacionCompleta = data;

        data.forEach((sucursal: Sucursal) => {
          this.sucursales.push({
            idSucursal: sucursal.idSucursal,
            nombre: sucursal.nombre,
            telefono: sucursal.telefono,
            idDomicilio: sucursal.idDomicilio,
            colonias: sucursal.colonias
          });

          sucursal.colonias.forEach(colonia => {
            if (
              typeof colonia === 'object' &&
              colonia !== null &&
              'idColonia' in colonia &&
              !this.colonias.find(c => c.idColonia === colonia.idColonia)
            ) {
              this.colonias.push(colonia as Colonia);
            }

            if (
              typeof colonia === 'object' &&
              colonia !== null &&
              'ciudad' in colonia
            ) {
              const ciudad = colonia.ciudad;
              if (!this.ciudades.find(c => c.idCiudad === ciudad.idCiudad)) {
                this.ciudades.push(ciudad);
              }

              const municipio = ciudad.municipio;
              if (!this.municipios.find(m => m.idMunicipio === municipio.idMunicipio)) {
                this.municipios.push(municipio);
              }

              const estado = municipio.estado;
              if (!this.estados.find(e => e.idEstado === estado.idEstado)) {
                this.estados.push(estado);
              }
            }
          });
        });
      },
      error: (err) => console.error('Error cargando ubicación completa:', err)
    });

    // Cargar servicios disponibles
    this.http.get<any[]>('http://localhost:5267/api/Servicios').subscribe({
      next: (data) => {
        this.servicios = data;
      },
      error: (err) => console.error('Error cargando servicios:', err)
    });
  }

  seleccionarEstado(estado: Estado) {
    this.estadoSeleccionado = estado;
    this.municipioSeleccionado = this.ciudadSeleccionada = this.coloniaSeleccionada = undefined;

    this.municipiosFiltrados = this.municipios.filter(m => m.estado.idEstado === estado.idEstado);
    this.ciudadesFiltradas = [];
    this.coloniasFiltradas = [];
    this.sucursalesFiltradas = [];
  }

  seleccionarMunicipio(municipio: Municipio) {
    this.municipioSeleccionado = municipio;
    this.ciudadSeleccionada = this.coloniaSeleccionada = undefined;

    this.ciudadesFiltradas = this.ciudades.filter(c => c.municipio.idMunicipio === municipio.idMunicipio);
    this.coloniasFiltradas = [];
    this.sucursalesFiltradas = [];
  }

  seleccionarCiudad(ciudad: Ciudad) {
    this.ciudadSeleccionada = ciudad;
    this.coloniaSeleccionada = undefined;

    this.coloniasFiltradas = this.colonias.filter(col => col.ciudad.idCiudad === ciudad.idCiudad);
    this.sucursalesFiltradas = [];
  }

  seleccionarColonia(colonia: Colonia) {
    this.coloniaSeleccionada = colonia;

    this.sucursalesFiltradas = this.sucursales.filter(s =>
      (s.colonias as Colonia[]).some(col => col.idColonia === colonia.idColonia)
    );
  }

  seleccionarServicio(servicio: string) {
    this.servicioSeleccionado = this.servicioSeleccionado === servicio ? null : servicio;
  }

  ajustarAltura() {
    const el = this.textarea.nativeElement;
    el.style.height = 'auto';
    el.style.height = el.scrollHeight + 'px';
  }

  guardarPromocion() {
    // Validaciones básicas
    if (!this.nombre || !this.descripcionDescuento || !this.fechaInicio || !this.fechaFin) {
      alert('Por favor completa todos los campos obligatorios');
      return;
    }

    if (!this.servicioSeleccionado) {
      alert('Por favor selecciona un servicio');
      return;
    }

    if (!this.estadoSeleccionado) {
      alert('Por favor selecciona al menos un estado para el alcance');
      return;
    }

    const tipo_descuento = this.tipoDescuento === 'porcentaje'
      ? 'PORCENTAJE'
      : this.tipoDescuento === 'monto'
        ? 'MONTO_FIJO'
        : 'MESES_GRATIS';

    const aplica_a = this.aplicaMensualidad
      ? 'MENSUALIDAD'
      : 'INSTALACION';

    const valor_descuento = tipo_descuento === 'MESES_GRATIS'
      ? this.mesesGratis
      : this.valorDescuento;

    const duracion_meses = tipo_descuento === 'MESES_GRATIS'
      ? this.mesesGratis
      : this.duracionSeleccionada;

    // Mapear el servicio seleccionado a un ID real
    let servicioId = 1; // Default
    if (this.servicioSeleccionado) {
      // Buscar el servicio basado en el nombre corto seleccionado
      const servicioEncontrado = this.servicios.find(s => {
        const nombreCorto = this.obtenerNombreCorto(s.nombre);
        return nombreCorto === this.servicioSeleccionado;
      });
      
      if (servicioEncontrado) {
        servicioId = servicioEncontrado.idServicio;
      } else {
        // Si no encuentra coincidencia, mostrar error
        const serviciosDisponibles = this.servicios.map(s => `${this.obtenerNombreCorto(s.nombre)} (${s.nombre})`).join(', ');
        alert(`Error: No se encontró el servicio "${this.servicioSeleccionado}" en la base de datos. Servicios disponibles: ${serviciosDisponibles}`);
        return;
      }
    }

    // Crear alcances basados en la ubicación seleccionada
    const alcances = [];

    if (this.estadoSeleccionado) {
      const alcance: any = {
        idEstado: this.estadoSeleccionado.idEstado,
        idMunicipio: null,
        idCiudad: null,
        idColonia: null,
        idSucursal: null
      };

      if (this.municipioSeleccionado) {
        alcance.idMunicipio = this.municipioSeleccionado.idMunicipio;
      }

      if (this.ciudadSeleccionada) {
        alcance.idCiudad = this.ciudadSeleccionada.idCiudad;
      }

      if (this.coloniaSeleccionada) {
        alcance.idColonia = this.coloniaSeleccionada.idColonia;
      }

      // Si hay sucursales filtradas, crear un alcance por cada una
      if (this.sucursalesFiltradas.length > 0) {
        this.sucursalesFiltradas.forEach(sucursal => {
          alcances.push({
            ...alcance,
            idSucursal: sucursal.idSucursal
          });
        });
      } else {
        alcances.push(alcance);
      }
    }

    const promocion = {
      nombre: this.nombre,
      descripcion: this.descripcionDescuento,
      fechaInicio: new Date(this.fechaInicio).toISOString(),
      fechaFin: new Date(this.fechaFin).toISOString(),
      tipoDescuento: tipo_descuento,
      valorDescuento: valor_descuento,
      aplicaA: aplica_a,
      duracionMeses: duracion_meses,
      idServicios: [servicioId],
      alcances
    };

    console.log('Promoción a enviar:', promocion);
    console.log('Servicio seleccionado:', this.servicioSeleccionado);
    console.log('ID del servicio:', servicioId);

    // Prueba temporal: enviar al endpoint de test
    this.http.post('http://localhost:5267/api/Promociones/test-frontend', promocion)
      .subscribe({
        next: (res) => {
          console.log('Respuesta del endpoint de test:', res);
        },
        error: (err) => {
          console.error('Error en endpoint de test:', err);
        }
      });

    // Mostrar indicador de carga
    const botonGuardar = document.querySelector('.boton') as HTMLButtonElement;
    if (botonGuardar) {
      botonGuardar.textContent = 'Guardando...';
      botonGuardar.disabled = true;
    }

    this.http.post('http://localhost:5267/api/Promociones/crear-completa', promocion)
      .subscribe({
        next: (res) => {
          // Restaurar botón
          if (botonGuardar) {
            botonGuardar.textContent = 'Guardar';
            botonGuardar.disabled = false;
          }

          // Mostrar mensaje de éxito
          const servicioEncontrado = this.servicios.find(s => s.nombre === this.servicioSeleccionado);
          const respuesta = res as any;
          const mensajeExito = `
            ✅ Promoción guardada exitosamente!
            
            📋 Detalles:
            • Nombre: ${this.nombre}
            • Servicio: ${this.servicioSeleccionado} (ID: ${servicioEncontrado?.idServicio || 'N/A'})
            • Alcance: ${this.estadoSeleccionado?.nombre}
            • Fecha inicio: ${this.fechaInicio}
            • Fecha fin: ${this.fechaFin}
            • Servicios asociados: ${respuesta?.ServiciosAsociados || 0}
            • Alcances creados: ${respuesta?.AlcancesCreados || 0}
            
            🔗 ID de promoción: ${respuesta?.IdPromocion || 'N/A'}
          `;

          alert(mensajeExito);

          // Limpiar formulario después de confirmar
          if (confirm('¿Deseas crear otra promoción?')) {
            this.limpiarFormulario();
          } else {
            this.limpiarFormulario();
          }
        },
        error: (err) => {
          // Restaurar botón
          if (botonGuardar) {
            botonGuardar.textContent = 'Guardar';
            botonGuardar.disabled = false;
          }

          // Mostrar mensaje de error
          const mensajeError = `
            Error al guardar la promoción (Código: ${err.status})
            
            Detalles del error:
            • Código: ${err.status || 'N/A'}
            • Mensaje: ${err.message || 'Error desconocido'}
            • Respuesta: ${err.error ? JSON.stringify(err.error) : 'N/A'}
            
            💡 Verifica:
            • Que los datos enviados sean válidos
            • Que las fechas estén en formato correcto (YYYY-MM-DD)
            • Que los IDs de servicios existan en la base de datos
          `;

          alert(mensajeError);
        }
      });
  }

  limpiarFormulario() {
    this.nombre = '';
    this.descripcionDescuento = '';
    this.fechaInicio = '';
    this.fechaFin = '';
    this.tipoDescuento = 'monto';
    this.valorDescuento = 0;
    this.mesesGratis = 1;
    this.duracionSeleccionada = 1;
    this.aplicaMensualidad = false;
    this.aplicaInstalacion = false;
    this.servicioSeleccionado = null;
    this.estadoSeleccionado = undefined;
    this.municipioSeleccionado = undefined;
    this.ciudadSeleccionada = undefined;
    this.coloniaSeleccionada = undefined;
    this.municipiosFiltrados = [];
    this.ciudadesFiltradas = [];
    this.coloniasFiltradas = [];
    this.sucursalesFiltradas = [];
  }

  obtenerNombreCorto(nombreCompleto: string): string {
    // Normalizar el texto para manejar caracteres especiales
    const nombreNormalizado = nombreCompleto.toLowerCase()
      .normalize('NFD')
      .replace(/[\u0300-\u036f]/g, ''); // Remover acentos
    
    if (nombreNormalizado.includes('internet')) {
      return 'Internet';
    } else if (nombreNormalizado.includes('tv') || nombreNormalizado.includes('television')) {
      return 'TV';
    } else if (nombreNormalizado.includes('telefonia') || nombreNormalizado.includes('telefono')) {
      return 'Telefonía';
    }
    return nombreCompleto; // Si no coincide con ninguno, devuelve el nombre original
  }

  esFormularioInvalido(): boolean {
    if (!this.nombre || !this.descripcionDescuento || !this.fechaInicio || !this.fechaFin) return true;

    if (!this.servicioSeleccionado || !this.estadoSeleccionado) return true;

    if (this.tipoDescuento === 'porcentaje') {
      return this.valorDescuento == null || this.valorDescuento < 0 || this.valorDescuento > 100;
    }

    if (this.tipoDescuento === 'monto') {
      return this.valorDescuento == null || this.valorDescuento < 0;
    }

    if (this.tipoDescuento === 'gratis') {
      return this.mesesGratis == null || this.mesesGratis < 1 || this.mesesGratis > 12;
    }

    return false;
  }

}
