import { EditorVal, indexModel } from "./core.model";

export class ArticleRequestDTO {
    id: number;
    title: string;
    titleHindi: string;
    articleType: number;
    summary: string;
    summaryHindi: string;
    description: string;
    descriptionHindi: string;
    descriptionJson: string;
    descriptionJsonHindi: string;
    keywords: string;
    keywordHindi: string;
    thumbnail: string;
    thumbnailCredit: string;
    slugUrl: string;
    articleTagsDTOs: number[];
    articleFaqsDTOs: ArticleFaqDTO[];
}

export class ArticleTagsDTO {
    tagsId: number;
}

export class ArticleFaqDTO {
    id: number;
    articleId: number;
    que: string;
    ans: string;
    queHindi: string;
    ansHindi: string;
}

export class ArticleResponseDTO {
    id: number;
    title: string;
    titleHindi: string;
    articleType: number;
    summary: string;
    summaryHindi: string;
    description:  EditorVal | null;;
    descriptionHindi:  EditorVal | null;;
    descriptionJson: string;
    descriptionJsonHindi: string;
    keywords: string;
    keywordHindi: string;
    thumbnail: string;
    thumbnailCredit: string;
    slugUrl: string;
    articleTagsDTOs: number[];
    publisherDate: any;
    articleFaqsDTOs: ArticleFaqResponseDTO[]=[];
}

export class ArticleTagsResponseDTO {
    tagsId: number;
}

export class ArticleFaqResponseDTO {
    id: number;
    que: string;
    ans: string;
    queHindi: string;
    ansHindi: string;
}

export class ArticleTitleCheckDTO {
    title: string | null;
    slugUrl: string;
    status: string;
    isActive: boolean;
}

export class ArticleViewListDTO {
    id: number;
    title: string;
    titleHindi: string;
    slugUrl: string;
    modifiedByName: string;
    modifiedDate: string;
    status: number;
    visitCount: number;
    publisherName: string;
    publisherDate: string;
    isActive: boolean;
}

export class ArticleFilterDTO extends indexModel {
    title: string | null;
    titleHindi: string | null;
    fromDate: string | null;
    toDate: string | null;
}