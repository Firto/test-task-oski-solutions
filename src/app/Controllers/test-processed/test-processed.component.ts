import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Toaster } from 'ngx-toast-notifications';
import { filter, switchMap } from 'rxjs/operators';
import { SessionService } from 'src/app/Services/session.service';
import { TestsService } from 'src/app/Services/tests.service';

@UntilDestroy()
@Component({
  selector: 'app-test-processed',
  templateUrl: './test-processed.component.html',
  styleUrls: ['./test-processed.component.css'],
  providers: [
    SessionService
  ]
})
export class TestProcessedComponent implements OnInit {
  id: string;

  constructor(
    public sessionService: SessionService,
    private router: Router,
    private activateRoute: ActivatedRoute,
    private toastrService: Toaster) {
  }

  public isSelected(qid: number): boolean {
    return this.sessionService.session.questions[qid].options.find(o => o.selected) !== undefined;
  }

  public select(qid: number): void {
    this.sessionService.selectQuestion(qid);
    this.router.navigate(['/', 'test', this.id, qid]);
  }

  public finishTest(): void {
    this.sessionService.endSession().subscribe(ses => {
      this.router.navigate(['/', 'test', ses.id, 'result']);
      this.toastrService.open("Test finished!", { type: "success" });
    });
  }

  public validateSession(session: any): boolean
  {
    const date: Date = new Date(new Date(session.startDateTime).getTime() + session.test.testLengthInMinuts*60000);
    return date.getTime() >= Date.now();
  }

  ngOnInit(): void {
    this.activateRoute.paramMap
      .pipe(untilDestroyed(this))
      .subscribe(params => {
        if (this.id != params.get('id'))
          this.sessionService.initSession(params.get('id')).subscribe(ses => {
            if (ses.endDateTime && !this.router.url.includes("/result"))
            {
              this.router.navigate(['/', 'test', ses.id, 'result']);
              this.toastrService.open("Session is expired!", { type: "danger" });
              return;
            }
            this.id = ses.id;
          });
      });
  }

}
