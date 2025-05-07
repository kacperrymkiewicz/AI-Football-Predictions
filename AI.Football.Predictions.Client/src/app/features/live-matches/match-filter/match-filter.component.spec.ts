import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchFilterComponent } from './match-filter.component';

describe('MatchFilterComponent', () => {
  let component: MatchFilterComponent;
  let fixture: ComponentFixture<MatchFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MatchFilterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MatchFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
