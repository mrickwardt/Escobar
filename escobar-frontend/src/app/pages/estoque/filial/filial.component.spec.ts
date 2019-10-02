import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FilialComponent } from './filial.component';

describe('FilialComponent', () => {
  let component: FilialComponent;
  let fixture: ComponentFixture<FilialComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FilialComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FilialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
