import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdatePaperComponent } from './add-update-paper.component';

describe('AddUpdatePaperComponent', () => {
  let component: AddUpdatePaperComponent;
  let fixture: ComponentFixture<AddUpdatePaperComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddUpdatePaperComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddUpdatePaperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
