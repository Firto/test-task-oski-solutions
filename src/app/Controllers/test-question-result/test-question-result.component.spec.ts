import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestQuestionResultComponent } from './test-question-result.component';

describe('TestQuestionResultComponent', () => {
  let component: TestQuestionResultComponent;
  let fixture: ComponentFixture<TestQuestionResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestQuestionResultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestQuestionResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
