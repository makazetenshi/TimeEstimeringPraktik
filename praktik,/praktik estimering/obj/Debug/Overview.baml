   M S B A M L   `   `   `  ����  praktik estimering!    praktik_estimering.Overview   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$   Time Estimation - Overview��5   	   $	��299��6,   $	��332��69    � 
ResizeMode$ NoResize=�6]   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��227* �6   �� 5      $
��106* �6    � RowDefinitions 5
   
   �� 5      $	��19* �6   �� 5      $
��116* �6   .+�5   
   �� $��Periods��6   $
��Left=�6"   $��	10,10,0,0q�6=   $	��Top=�6P   $��28��6h   $��79��6t   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ datagridOldPeriods��5      $
��Left=�6/   $��	10,10,0,0q�6J   $��1��6]   $	��Top=�6j   $	��212��6�   $	��201��6�      
IsReadOnly . 6�   �� 5   
   -   5         ��Name$ buttonNewPeriod��5      $��
New period��6*   $��1��6?   $
��Left=�6O   $��12.4,9.6,0,0q�6j   $��1��6�   $	��Top=�6�   $��84��6�   $��22��6�   6�   �� 5   
   -   5        $ buttonViewOld��5      $��	View Info��6(   $��1��6<   $
��Left=�6L   $��9.4,54.6,0,0q�6g   $��1��6}   $	��Top=�6�   $��85��6�   $��22��6�   6�   �� 5   
   -   5        $ buttonChanges��5      $��Update Period��6(   $��1��6@   $
��Left=�6P   $��11.4,81.6,0,0q�6k   $��1��6�   $	��Top=�6�   $��85��6�   $��22��6�   �� 5   
   -   5        $ buttonLogout��5      $��Logout��6'   $��1��68   $
��Left=�6H   $��10.4,199.6,0,0q�6c   $��1��6{   $	��Top=�6�   $��84��6�   $��22��6�   5      5      