import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import { progressDdl } from 'src/app/core/models/fixed-value';
import { NotesFilterDTO, NotesList } from 'src/app/core/models/notes.model';
import { NotesService } from 'src/app/core/services/notes.service';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.scss']
})
export class NotesComponent implements OnInit {
  initialData: any;
  filter: NotesFilterDTO = new NotesFilterDTO();
  breadcrumb: Breadcrumb[] = [];
  columns: columns;
  notes: ApiResponseModel<NotesList[]>
  constructor(
    private _route: ActivatedRoute,
    public _notesService: NotesService
  ) { }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
    let initialData = this._route.snapshot.data['initialData'];
    this.notes = initialData.notes;
    this.addColumns();
  }

  addColumns() {
    this.columns = [
      {
        columnKey: 'title',
        columnText: 'Title',
        searchType: 'text',
      },
      // {
      //   columnKey: 'departmentName',
      //   columnText: 'Department Name',
      //   filterKey: 'departmentId',
      //   searchType: 'singleSelect',
      //   filterOptions: this.ddls['ddlDepartment'],
      // },
      // {
      //   columnKey: 'blockTypeName',
      //   columnText: 'block Type',
      // },
      // {
      //   columnKey: 'categoryName',
      //   columnText: 'Category',
      //   filterKey: 'categoryId',
      //   searchType: 'singleSelect',
      //   filterOptions: this.ddls['ddlCategory'],
      // },

      // {
      //   columnKey: 'subCategoryName',
      //   columnText: 'Sub Category',
      //   filterKey: 'subCategoryId',
      //   searchType: 'singleSelect',
      //   filterOptions: this.ddls['ddlSubCategory'],
      // },
      // {
      //   columnKey: 'startDate',
      //   columnText: 'Start Date',
      //   sorting: true,
      //   type: 'date',
      // },
      {
        columnKey: 'visitCount',
        columnText: 'Visit',
        type: 'text',
      },
      // {
      //   columnKey: 'lastDate',
      //   columnText: 'Last Date',
      //   sorting: true,
      //   type: 'date',
      // },
      {
        columnKey: 'publisherName',
        columnText: 'Publisher',
        filterKey: 'publisherId',
        searchType: 'singleSelect',
      },
      {
        columnKey: 'publishedDate',
        columnText: 'Published Date',
        sorting: true,
        type: 'date',
      },
      {
        columnKey: 'slugUrl',
        columnText: 'Slug Url',
        type: 'siteView',
        isHide: true,
      },
      {
        columnKey: 'modifiedByName',
        columnText: 'Modified By',
      },
      {
        columnKey: 'modifiedDate',
        columnText: 'Modified Date',
        sorting: true,
        type: 'date',
      },
      {
        columnKey: 'status',
        columnText: 'Update Progress',
        type: 'updateProgress',
        statusDdl: progressDdl,
        columnWidth: '150px',
        columnAlign: 'right',
      },
      {
        columnKey: 'copySlugUrl',
        columnText: 'Attachment',
        type: 'copyIcon',
        isHide: true,
      },
    ];
  }



}
