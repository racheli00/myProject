   M S B A M L   `   `   `  ����G  CtastyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:tastyProjecttastyProject  5   	   &     tastyProject.specific_Categories   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:tastyProject     5   	   -   5          � Title$  specific_Categories��5	   	   $	��450��6%   $	��800��62    � WindowStartupLocation$ CenterScreen=�6?    � WindowState$ 	Maximized=�6d    � 
ResizeMode$ 	CanResize=�6|   .��5
      �� $
��Auto=�6   $��20��66   .��5   
   �  � RowDefinitions 5      �� 5      $��70 �6    6.   �� 5      $��50 �6    6.   �� 5      $��* �6    6-   ��5      �� 5      .+�5      ]� $��0��6   ���#c32026$��70��6:   $	��Top=�6F   .+�5      �� -   5         ��Name$
 logo��5      $��0,3,0,3q�6&   $	��150��67   $
��Left=�6C   $��images\design\Logo.jpg��6^   $
��Fill=�6~    � ToolTip$  חזרה לדף הראשי��6�   6�   �� 5      -   5        $ signOff��5      $��50��6)   $��	5,15,5,15q�64   $��Center=�6G   $��images\design\profile.png��6d   $
��Fill=�6�   $ 
התנתק��6�   6�   �� 5      -   5        $ likedRecipe��5      $��50��6-   $��	5,15,5,15q�68   $��Center=�6K   $%��images\design\liked-recipes.png��6h   $
��Fill=�6�   $ מתכונים שאהבת��6�   6�   �� 5      -   5        $ lastRecipes��5      $��50��6-   $��	5,15,5,15q�68   $��Center=�6K   $$��images\design\last-recipes.png��6h   $
��Fill=�6�   $# מתכונים אחרונים��6�   6�   �� 5      -   5        $ search��5      $��50��6(   $��	5,15,5,15q�63   $
��Left=�6F   $#��images\design\search-cart.png��6a   $
��Fill=�6�   $&  לחיפוש לפי מוצרים��6�   6�   �� 5      -   5        $ return��5      $��50��6(   $��	5,15,5,15q�63   $��Right=�6F   $��images\design\return.png��6b   $
��Fill=�6�   $  חזרה לדף הקודם��6�   6�   5      �� 5      -   5        $��	pageTitle��5   "   $��1��6   $��	pageTitle��63   $��Center=�6G   $	��Top=�6d   $*��$/tastyProject;component/Fonts/#Heebo)�6|   $��30(�6�   $��RightToLeft=�6�   $��50��6�   �� 5      -	   5        $��
resultsNum��5   !   $��1��6   $��X תוצאות חיפוש��63   $
��Left=�6L   $	��top=�6g   $*��$/tastyProject;component/Fonts/#Heebo)�6   $��15(�6�   $��35��6�   $��RightToLeft=�6�   $
��Left=�6�   � 5!      -
   5         �Name$
 grid��5!       $��2��6   $
��auto��6.   $��50��6<   $��Stretch=�6K   $
��auto��6g   $	��200��6t   $��50,20,50,20q�6�    � ColumnDefinitions 5"      �� 5#      $��60��6'   $	��60* �65   6C   �� 5$      $��60��6'   $	��60* �65   6C   �� 5%      $��60��6'   $	��60* �65   6C   5)      5*      5+      5,      