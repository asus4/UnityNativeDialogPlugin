//
//  UNDialogManager.h
//  UnityDialogPlugin
//
//  Created by ibu on 12/10/09.
//  Copyright (c) 2012年 kayac. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface UNDialogManager : NSObject<UIAlertViewDelegate> {
    int _id;
    NSMutableDictionary *alerts;

    NSString *decideLabel;
    NSString *cancelLabel;
    NSString *closeLabel;
}
+ (UNDialogManager*) sharedManager;
- (int) showSelectDialog:(NSString *)msg;
- (int) showSelectDialog:(NSString *)title message:(NSString*)msg;
- (int) showSubmitDialog:(NSString *)msg;
- (int) showSubmitDialog:(NSString *)title message:(NSString*)msg;
- (void) dissmissDialog:(int) theID;
- (void) setLabelTitleWithDecide:(NSString*)decide cancel:(NSString*)cancel close:(NSString*) close;
@end