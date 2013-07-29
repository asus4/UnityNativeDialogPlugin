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
}
+ (UNDialogManager*) sharedManager;
- (int) showSelectDialog:(NSString *)msg;
- (int) showSelectDialog:(NSString *)title message:(NSString*)msg;
- (int) showSubmitDialog:(NSString *)msg;
- (int) showSubmitDialog:(NSString *)title message:(NSString*)msg;
@end
