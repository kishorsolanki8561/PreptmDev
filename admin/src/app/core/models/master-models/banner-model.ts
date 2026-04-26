import { indexModel } from "../core.model";

export class BannerModel {
	id: number;
	title: string;
	titleHindi: string | null;
	url: string | null;
	isAdvt: boolean = false;
	attachmentUrl: string | null;
	displayOrder: number;
	constructor() {
		this.isAdvt = false;
	}
}

export class BannerViewModel {
	id: number;
	title: string;
	titleHindi: string | null;
	url: string | null;
	attachmentUrl: string | null;
	isAdvt: boolean;
	displayOrder: number;
}

export class BannerViewListModel {
	id: number;
	title: string;
	titleHindi: string | null;
	url: string | null;
	isAdvt: boolean;
	attachmentUrl: string;
	displayOrder: number;
	modifiedByName: string | null;
	modifiedDate: string;
	isActive: boolean;
}

export class BannerFilterModel extends indexModel {
	title: string;
	isAdvt: number;
	fromDate: string | null;
	toDate: string | null;
}