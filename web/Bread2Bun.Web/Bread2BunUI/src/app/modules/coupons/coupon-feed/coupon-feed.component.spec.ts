import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CouponFeedComponent } from './coupon-feed.component';

describe('CouponFeedComponent', () => {
  let component: CouponFeedComponent;
  let fixture: ComponentFixture<CouponFeedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CouponFeedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CouponFeedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
