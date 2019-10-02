import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MovimentoComponent } from './movimento.component';

describe('MovimentoComponent', () => {
  let component: MovimentoComponent;
  let fixture: ComponentFixture<MovimentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MovimentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MovimentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
