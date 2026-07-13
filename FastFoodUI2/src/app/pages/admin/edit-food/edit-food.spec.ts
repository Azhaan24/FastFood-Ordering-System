import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditFoodComponent } from './edit-food';

describe('EditFood', () => {
  let component: EditFoodComponent;
  let fixture: ComponentFixture<EditFoodComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditFoodComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EditFoodComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
