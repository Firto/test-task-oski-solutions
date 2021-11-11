import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-question-result',
  templateUrl: './test-question-result.component.html',
  styleUrls: ['./test-question-result.component.css']
})
export class TestQuestionResultComponent implements OnInit {
  @Input() question: any;
  constructor() { }

  ngOnInit(): void {
  }

}
