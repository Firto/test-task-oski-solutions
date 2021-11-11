import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestProcessedComponent } from './test-processed.component';

describe('TestProcessedComponent', () => {
  let component: TestProcessedComponent;
  let fixture: ComponentFixture<TestProcessedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestProcessedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestProcessedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
