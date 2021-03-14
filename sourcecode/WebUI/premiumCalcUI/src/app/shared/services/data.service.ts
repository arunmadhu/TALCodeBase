import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { AppConfiguration } from "read-appsettings-json";
import { premiumRequest } from '../model/premiumRequest';

@Injectable()
export class DataService {

  constructor(private http: HttpClient) {
  }

  getAllOccupations() {
    let service: string = "api/premium/occupations";

    service = AppConfiguration.Setting().ApplicationSettings.APIURL + service;
    return this.http.get(service);
  }

  calculatePremium(request: premiumRequest){
    let service: string = "api/premium";
    service = AppConfiguration.Setting().ApplicationSettings.APIURL + service;

    return this.http.post(service, request);
  }
}
