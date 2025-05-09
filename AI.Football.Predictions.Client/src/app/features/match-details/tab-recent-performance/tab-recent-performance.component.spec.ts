import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabRecentPerformanceComponent } from './tab-recent-performance.component';

describe('TabRecentPerformanceComponent', () => {
  let component: TabRecentPerformanceComponent;
  let fixture: ComponentFixture<TabRecentPerformanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TabRecentPerformanceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TabRecentPerformanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
