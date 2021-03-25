import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public terms: Term[];
  private defaultMortgage: MortgageInfoDTO;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    const defaultMortgage: MortgageInfoDTO = {
      BorrowedAmount: 100000,
      DurationMonths: 240,
      RatePercent: 1.2
    };

    http.post<Term[]>(baseUrl + 'mortgage/compute',  {
      BorrowedAmount: 100000,
      DurationMonths: 240,
      RatePercent: 1.2
     }).subscribe(result => {
      this.terms = result;
    }, error => console.error(error));
  }
}

interface Term {
  interest: number;
  totalAmount: number;
  amortizedCapital: number;
  remainingCapital: number;
}

interface MortgageInfoDTO {
  BorrowedAmount: number;
  DurationMonths: number;
  RatePercent: number;
}
