import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { filter, tap } from "rxjs/operators";
import { apiConfig } from "../Const/api.config";
import { Service } from "../Interfaces/Service/service.interface";
import { TestModel } from "../Models/Tests/test.model";
import { BaseService } from "./base.service";
import { TestsService } from "./tests.service";

@Injectable()
export class SessionService {
  public session$: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public selectedQuestion$: BehaviorSubject<{id: number}> = new BehaviorSubject<any>(null);
  public get session(){
    return this.session$.value;
  }
  public get selectedQuestion(){
    return this.selectedQuestion$.value;
  }
  constructor(private _testsService: TestsService) {
    this.session$.pipe(filter(ses => ses)).subscribe(new_ses => {
        this.selectQuestion(this.selectedQuestion?.id ?? 0);
    });
  }

  selectQuestion(id: number): void
  {
    if (!this.selectedQuestion || this.selectedQuestion.id != id)
      this.selectedQuestion$.next({id});
  }

  initSession(id: string): Observable<any> {
    return this._testsService.getSessionById(id).pipe(tap(ses => {
      this.session$.next(ses);
    }));
  }

  setQuestionOption(oid: string, selection: boolean): Observable<any>
  {
    return this._testsService.setQuestionOption(oid, selection).pipe(tap(opt =>
    {
      const question: any = this.session.questions.find(q => q.options.find(o => o.id == opt.id));
      const option = question.options.find(o => o.id == opt.id);
      if (option)
        Object.assign(option, opt);
    }));
  }

  endSession(): Observable<any>
  {
    return this._testsService.endSessionById(this.session.id).pipe(tap(ses =>
    {
      this.session$.next(ses);
    }));
  }
}
