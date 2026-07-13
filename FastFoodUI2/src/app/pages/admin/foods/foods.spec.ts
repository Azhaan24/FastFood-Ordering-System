import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodsComponent } from './foods';

describe('Foods', () => {
  let component: FoodsComponent;
  let fixture: ComponentFixture<FoodsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FoodsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(FoodsComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
