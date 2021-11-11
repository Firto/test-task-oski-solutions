import { Injectable } from "@angular/core";
import { BehaviorSubject, Subject } from "rxjs";

@Injectable()
export class DataService {
  public data:Subject<any> = new BehaviorSubject<any>(null);
}
