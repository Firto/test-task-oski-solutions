import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { SessionService } from 'src/app/Services/session.service';

@UntilDestroy()
@Component({
  selector: 'app-test-question',
  templateUrl: './test-question.component.html',
  styleUrls: ['./test-question.component.css']
})
export class TestQuestionComponent implements OnInit {

  constructor(private activateRoute: ActivatedRoute,
              public sessionService: SessionService) {}

  ngOnInit(): void
  {
    this.activateRoute.paramMap
      .pipe(untilDestroyed(this))
      .subscribe(params => {
        this.sessionService.selectQuestion(+params.get("questionId") ?? 0);
      });
  }

  toggleQuestionOption(id: number, ev: any): void
  {
    const option: any = this.sessionService.session.questions[this.sessionService.selectedQuestion.id].options[id];
    this.sessionService.setQuestionOption(option.id, !option.selected).subscribe();
  }
}
