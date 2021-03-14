import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { AppsettingsModel } from '../model/appsettings.model';
 
@Injectable()
export class AppsettingService {
  private appSettingsData: AppsettingsModel;
  constructor(private _http: HttpClient) { }


  loadData() {
    return this._http.get<AppsettingsModel>('../../assets/appSettings/appSettings.json')
      .toPromise()
      .then(result => {
        this.appSettingsData = (result) as AppsettingsModel;
      }, error => console.error(error));
  }
  get appSettings() {
    return this.appSettingsData;
  }
  get APIUrl() {
    return this.appSettingsData.ApplicationSettings.APIURL;
  }
}
