import { Component, OnInit } from '@angular/core';
import { TestModel } from 'src/app/Models/Tests/test.model';
import { TestsService } from 'src/app/Services/tests.service';

@Component({
  selector: 'app-list-of-tests',
  templateUrl: './list-of-tests.component.html',
  styleUrls: ['./list-of-tests.component.css']
})
export class ListOfTestsComponent implements OnInit {
  tests: Array<TestModel>;
  constructor(public testsService: TestsService) { }

  ngOnInit(): void {
    this.testsService.getAll().subscribe(res => {
      this.tests = res;
    });
  }

}
