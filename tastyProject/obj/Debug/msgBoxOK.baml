   M S B A M L   `   `   `  ����G  CtastyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:tastyProjecttastyProject  5   	       tastyProject.msgBoxOK   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:tastyProject     5   	     � Title$   ��5   	   $	��160��6   $	��350��6    � WindowStartupLocation$ CenterScreen=�6+    � WindowStyle$ 
ToolWindow=�6P   .��5	      � $	��120��6   $	��320��6    � RowDefinitions 5
   
   �� 5      $��80 �6   6*   �� 5      $��50 �6   6*   .+�5   
   �� -   5         ��Name$ textMsg��5      $0��*שם משתמש או סיסמא חסרים��6%   $��20(�6D   $1��+/tastyProject;component/Fonts/#Heebo Medium)�6R   $��	60,20,0,0q�6�    �� TextAlignment$ Center=�6�   $
��Wrap=�6�   
���   �$��2��6�   6�   �� 5   
   -   5         ��Name$ ok��5         CustomButtonStyle#��[  6    � Cursor$
 Hand��6H   $��1��6V   $��50��6c   $��10(�6n   $��25��6|   $
��Left=�6�   $��Center=�6�   $��5,0,0,15q�6�   ���	#FFC32026#��� ��6�   #��� ��67  .��6�  
אוקיי � 	Resources 5      	 �� 
TargetType
 �� Property �� Value��%^   '��       �� 5      "	 ��6   .�5      �� 
 �@��CornerRadius$ 5��65   5      5      5      5      5      