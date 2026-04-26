import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderTileComponent } from './header-tile.component';

describe('HeaderTileComponent', () => {
  let component: HeaderTileComponent;
  let fixture: ComponentFixture<HeaderTileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HeaderTileComponent]
    });
    fixture = TestBed.createComponent(HeaderTileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
