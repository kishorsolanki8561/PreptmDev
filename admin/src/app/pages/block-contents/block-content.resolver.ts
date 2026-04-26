import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { forkJoin, map, Observable } from 'rxjs';
import { DdlsList } from 'src/app/core/api';
import { ApiResponseModel } from 'src/app/core/models/core.model';
import { BlockContentsService } from 'src/app/core/services/block-contents.service';
import { CoreService } from 'src/app/core/services/core.service';
import { BlockContentsFilterModel } from 'src/app/core/models/block-contents-model';
import { Lookup } from 'src/app/core/models/fixed-value';

@Injectable()
export class BlockContentResolver implements Resolve<any> {
  constructor(
    private _blockContentsService: BlockContentsService,
    private _coreService: CoreService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [
      this._coreService.getDdls(DdlsList.blockContents.list),
      this._blockContentsService.getBlockContentsList(
        new BlockContentsFilterModel()
      ),
    ];
    return forkJoin(dataGetters).pipe(
      map((data) => {
        return {
          ddls: data[0],
          blockContent: data[1],
        };
      })
    );
  }
}

@Injectable()
export class BlockContentAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _blockContentsService: BlockContentsService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [
      this._coreService.getDdls(DdlsList.blockContents.addUpdate), this._coreService.GetDDLLookupDataByLookupTypeIdAndLookupType('', '', `${Lookup.BlockContentUrlLabel},${Lookup.IconClassQuickLinks},${Lookup.UpcomingCalendarGroup}`)
    ];
    if (route.params['id']) {
      dataGetters.push(
        this._blockContentsService.getBlockContentsById(
          Number(route.params['id'])
        )
      );
    }
    return forkJoin(dataGetters).pipe(
      map((data) => {
        return {
          ddls: data[0],
          lookupsData: data[1],
          blockContentData: data[2] ? data[2] : undefined,
        };
      })
    );
  }
}
