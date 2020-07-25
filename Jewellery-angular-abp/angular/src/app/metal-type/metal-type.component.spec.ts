import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MetalTypeComponent } from './metal-type.component';

describe('MetalTypeComponent', () => {
  let component: MetalTypeComponent;
  let fixture: ComponentFixture<MetalTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MetalTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MetalTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
