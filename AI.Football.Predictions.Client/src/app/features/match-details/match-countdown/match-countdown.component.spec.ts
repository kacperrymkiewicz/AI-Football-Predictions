import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchCountdownComponent } from './match-countdown.component';

describe('MatchCountdownComponent', () => {
  let component: MatchCountdownComponent;
  let fixture: ComponentFixture<MatchCountdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MatchCountdownComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MatchCountdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
