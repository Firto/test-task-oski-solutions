import { Component, OnInit } from '@angular/core';
import { SessionService } from 'src/app/Services/session.service';

@Component({
  selector: 'app-test-result',
  templateUrl: './test-result.component.html',
  styleUrls: ['./test-result.component.css']
})
export class TestResultComponent implements OnInit {

  constructor(
    public sessionService: SessionService
  )
  {

  }

  ngOnInit(): void {
  }

}
