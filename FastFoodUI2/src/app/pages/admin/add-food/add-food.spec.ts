import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFoodComponent } from './add-food';

describe('AddFood', () => {
  let component: AddFoodComponent;
  let fixture: ComponentFixture<AddFoodComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddFoodComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AddFoodComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
