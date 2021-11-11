import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestProccedPageComponent } from './test-procced-page.component';

describe('TestProccedPageComponent', () => {
  let component: TestProccedPageComponent;
  let fixture: ComponentFixture<TestProccedPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestProccedPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestProccedPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
