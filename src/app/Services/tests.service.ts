import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { apiConfig } from "../Const/api.config";
import { Service } from "../Interfaces/Service/service.interface";
import { TestModel } from "../Models/Tests/test.model";
import { BaseService } from "./base.service";

@Injectable()
export class TestsService {
  private ser: Service;

  constructor(private _baseService: BaseService) {
      this.ser = apiConfig["tests"];
  }

  getAll(): Observable<Array<TestModel>>
  {
      return this._baseService.send<Array<TestModel>>(this.ser, "getall");
  }

  getById(id: number): Observable<TestModel>
  {
    return this._baseService.send<TestModel>(this.ser, "getbyid", null, { params: { id: id.toString() } });
  }

  start(id: number): Observable<any>
  {
    return this._baseService.send<any>(this.ser, "start", null, { params: { id: id.toString() } });
  }

  getSessionById(id: string): Observable<any>
  {
    return this._baseService.send<any>(this.ser, "getsessionbyid", null, { params: { id: id.toString() } });
  }

  setQuestionOption(oid: string, selection: boolean): Observable<any>
  {
    return this._baseService.send<any>(this.ser, "setquestionoption", { optionId: oid, selection });
  }

  endSessionById(id: string): Observable<any>
  {
    return this._baseService.send<any>(this.ser, "endsessionbyid", null, { params: { id: id.toString() } });
  }
}
