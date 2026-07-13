import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodDetailsComponent } from './food-details';

describe('FoodDetails', () => {
  let component: FoodDetailsComponent;
  let fixture: ComponentFixture<FoodDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FoodDetailsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(FoodDetailsComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
