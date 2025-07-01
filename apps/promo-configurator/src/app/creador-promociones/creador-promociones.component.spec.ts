import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreadorPromocionesComponent } from './creador-promociones.component';

describe('CreadorPromocionesComponent', () => {
  let component: CreadorPromocionesComponent;
  let fixture: ComponentFixture<CreadorPromocionesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreadorPromocionesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreadorPromocionesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
